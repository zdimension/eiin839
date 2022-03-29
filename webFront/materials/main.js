let stations = [];

function input(name)
{
    return document.getElementById(name).value;
}

function queryString(params)
{
    return new URLSearchParams({apiKey: getApiKey(), ...params}).toString();
}

function api(endpoint, params, callback)
{
    const caller = new XMLHttpRequest();
    caller.open("GET", `https://api.jcdecaux.com/vls/v3/${endpoint}?${queryString(params)}`, true);
    caller.setRequestHeader("Accept", "application/json");
    caller.onload = callback;
    caller.send();
}

function retrieveAllContracts()
{
    api("contracts", {}, feedContractList);
}

function retrieveContractStations()
{
    api("stations", {contract: document.getElementById("chosenContract").value}, feedStationList);
}

function feedContractList()
{
    if (this.status !== 200)
    {
        console.error("Unable to load contracts");
        return;
    }

    const contracts = JSON.parse(this.responseText).map(({name}) => name);
    const listContainer = document.getElementById("contractsList");
    listContainer.innerHTML = "";

    for (const currentContract of contracts)
    {
        const option = document.createElement("option")
        option.value = currentContract;
        listContainer.appendChild(option);
    }
}

function feedStationList()
{
    if (this.status !== 200)
    {
        console.error("Unable to load stations");
        return;
    }

    stations = JSON.parse(this.responseText);
}

function getApiKey()
{
    return input("apiKey");
}

function getClosestStation()
{
    const latitude = input("latitude");
    const longitude = input("longitude");

    let minDist = 1 / 0; // infinity
    let minStat = null;

    for (const currentStation of stations)
    {
        const distance = getDistanceFrom2GpsCoordinates(
            latitude, longitude,
            currentStation.position.latitude, currentStation.position.longitude);

        if (distance < minDist)
        {
            minDist = distance;
            minStat = currentStation.name;
        }
    }

    document.getElementById("closestStation").innerText = minStat;
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2)
{
    const earthRadius = 6371;
    const dLat = deg2rad(lat2 - lat1);
    const dLon = deg2rad(lon2 - lon1);
    const a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
    ;
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return earthRadius * c;
}

function deg2rad(deg)
{
    return deg * Math.PI / 180;
}
<?php

var_dump($_REQUEST);

$prenom = $_GET['prenom'];
$nom = $_GET['nom'];

echo "Bonjour $prenom $nom depuis PHP";
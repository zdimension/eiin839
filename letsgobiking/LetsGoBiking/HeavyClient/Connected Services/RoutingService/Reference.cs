﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeavyClient.RoutingService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JCDecauxStation", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class JCDecauxStation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string addressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool bankingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool bonusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool connectedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contractNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int numberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxPosition positionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string statusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxStandInfo totalStandsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string address {
            get {
                return this.addressField;
            }
            set {
                if ((object.ReferenceEquals(this.addressField, value) != true)) {
                    this.addressField = value;
                    this.RaisePropertyChanged("address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool banking {
            get {
                return this.bankingField;
            }
            set {
                if ((this.bankingField.Equals(value) != true)) {
                    this.bankingField = value;
                    this.RaisePropertyChanged("banking");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool bonus {
            get {
                return this.bonusField;
            }
            set {
                if ((this.bonusField.Equals(value) != true)) {
                    this.bonusField = value;
                    this.RaisePropertyChanged("bonus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool connected {
            get {
                return this.connectedField;
            }
            set {
                if ((this.connectedField.Equals(value) != true)) {
                    this.connectedField = value;
                    this.RaisePropertyChanged("connected");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contractName {
            get {
                return this.contractNameField;
            }
            set {
                if ((object.ReferenceEquals(this.contractNameField, value) != true)) {
                    this.contractNameField = value;
                    this.RaisePropertyChanged("contractName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int number {
            get {
                return this.numberField;
            }
            set {
                if ((this.numberField.Equals(value) != true)) {
                    this.numberField = value;
                    this.RaisePropertyChanged("number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxPosition position {
            get {
                return this.positionField;
            }
            set {
                if ((object.ReferenceEquals(this.positionField, value) != true)) {
                    this.positionField = value;
                    this.RaisePropertyChanged("position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string status {
            get {
                return this.statusField;
            }
            set {
                if ((object.ReferenceEquals(this.statusField, value) != true)) {
                    this.statusField = value;
                    this.RaisePropertyChanged("status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxStandInfo totalStands {
            get {
                return this.totalStandsField;
            }
            set {
                if ((object.ReferenceEquals(this.totalStandsField, value) != true)) {
                    this.totalStandsField = value;
                    this.RaisePropertyChanged("totalStands");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JCDecauxPosition", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class JCDecauxPosition : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double latitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double longitudeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double latitude {
            get {
                return this.latitudeField;
            }
            set {
                if ((this.latitudeField.Equals(value) != true)) {
                    this.latitudeField = value;
                    this.RaisePropertyChanged("latitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double longitude {
            get {
                return this.longitudeField;
            }
            set {
                if ((this.longitudeField.Equals(value) != true)) {
                    this.longitudeField = value;
                    this.RaisePropertyChanged("longitude");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JCDecauxStandInfo", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class JCDecauxStandInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxStandAvailability availabilitiesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int capacityField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxStandAvailability availabilities {
            get {
                return this.availabilitiesField;
            }
            set {
                if ((object.ReferenceEquals(this.availabilitiesField, value) != true)) {
                    this.availabilitiesField = value;
                    this.RaisePropertyChanged("availabilities");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int capacity {
            get {
                return this.capacityField;
            }
            set {
                if ((this.capacityField.Equals(value) != true)) {
                    this.capacityField = value;
                    this.RaisePropertyChanged("capacity");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JCDecauxStandAvailability", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class JCDecauxStandAvailability : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int bikesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int standsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int bikes {
            get {
                return this.bikesField;
            }
            set {
                if ((this.bikesField.Equals(value) != true)) {
                    this.bikesField = value;
                    this.RaisePropertyChanged("bikes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int stands {
            get {
                return this.standsField;
            }
            set {
                if ((this.standsField.Equals(value) != true)) {
                    this.standsField = value;
                    this.RaisePropertyChanged("stands");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RouteParameters", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class RouteParameters : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxPosition endField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxPosition startField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxPosition end {
            get {
                return this.endField;
            }
            set {
                if ((object.ReferenceEquals(this.endField, value) != true)) {
                    this.endField = value;
                    this.RaisePropertyChanged("end");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxPosition start {
            get {
                return this.startField;
            }
            set {
                if ((object.ReferenceEquals(this.startField, value) != true)) {
                    this.startField = value;
                    this.RaisePropertyChanged("start");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GeocodeParameters", Namespace="http://schemas.datacontract.org/2004/07/RoutingService")]
    [System.SerializableAttribute()]
    public partial class GeocodeParameters : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HeavyClient.RoutingService.JCDecauxPosition focusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string queryField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HeavyClient.RoutingService.JCDecauxPosition focus {
            get {
                return this.focusField;
            }
            set {
                if ((object.ReferenceEquals(this.focusField, value) != true)) {
                    this.focusField = value;
                    this.RaisePropertyChanged("focus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string query {
            get {
                return this.queryField;
            }
            set {
                if ((object.ReferenceEquals(this.queryField, value) != true)) {
                    this.queryField = value;
                    this.RaisePropertyChanged("query");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RoutingService.IBikeRoutingService")]
    public interface IBikeRoutingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetStations", ReplyAction="http://tempuri.org/IBikeRoutingService/GetStationsResponse")]
        HeavyClient.RoutingService.JCDecauxStation[] GetStations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetStations", ReplyAction="http://tempuri.org/IBikeRoutingService/GetStationsResponse")]
        System.Threading.Tasks.Task<HeavyClient.RoutingService.JCDecauxStation[]> GetStationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetStation", ReplyAction="http://tempuri.org/IBikeRoutingService/GetStationResponse")]
        HeavyClient.RoutingService.JCDecauxStation GetStation(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetStation", ReplyAction="http://tempuri.org/IBikeRoutingService/GetStationResponse")]
        System.Threading.Tasks.Task<HeavyClient.RoutingService.JCDecauxStation> GetStationAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetRoute", ReplyAction="http://tempuri.org/IBikeRoutingService/GetRouteResponse")]
        System.IO.Stream GetRoute(HeavyClient.RoutingService.RouteParameters points);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/GetRoute", ReplyAction="http://tempuri.org/IBikeRoutingService/GetRouteResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GetRouteAsync(HeavyClient.RoutingService.RouteParameters points);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/Geocode", ReplyAction="http://tempuri.org/IBikeRoutingService/GeocodeResponse")]
        System.IO.Stream Geocode(HeavyClient.RoutingService.GeocodeParameters geo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBikeRoutingService/Geocode", ReplyAction="http://tempuri.org/IBikeRoutingService/GeocodeResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GeocodeAsync(HeavyClient.RoutingService.GeocodeParameters geo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBikeRoutingServiceChannel : HeavyClient.RoutingService.IBikeRoutingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BikeRoutingServiceClient : System.ServiceModel.ClientBase<HeavyClient.RoutingService.IBikeRoutingService>, HeavyClient.RoutingService.IBikeRoutingService {
        
        public BikeRoutingServiceClient() {
        }
        
        public BikeRoutingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BikeRoutingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BikeRoutingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BikeRoutingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public HeavyClient.RoutingService.JCDecauxStation[] GetStations() {
            return base.Channel.GetStations();
        }
        
        public System.Threading.Tasks.Task<HeavyClient.RoutingService.JCDecauxStation[]> GetStationsAsync() {
            return base.Channel.GetStationsAsync();
        }
        
        public HeavyClient.RoutingService.JCDecauxStation GetStation(string id) {
            return base.Channel.GetStation(id);
        }
        
        public System.Threading.Tasks.Task<HeavyClient.RoutingService.JCDecauxStation> GetStationAsync(string id) {
            return base.Channel.GetStationAsync(id);
        }
        
        public System.IO.Stream GetRoute(HeavyClient.RoutingService.RouteParameters points) {
            return base.Channel.GetRoute(points);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GetRouteAsync(HeavyClient.RoutingService.RouteParameters points) {
            return base.Channel.GetRouteAsync(points);
        }
        
        public System.IO.Stream Geocode(HeavyClient.RoutingService.GeocodeParameters geo) {
            return base.Channel.Geocode(geo);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GeocodeAsync(HeavyClient.RoutingService.GeocodeParameters geo) {
            return base.Channel.GeocodeAsync(geo);
        }
    }
}

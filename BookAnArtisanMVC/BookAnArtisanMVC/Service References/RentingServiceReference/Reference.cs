﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookAnArtisanMVC.RentingServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Rented", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Rented : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BookAnArtisanMVC.RentingServiceReference.Material itemField;
        
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
        public bool Deleted {
            get {
                return this.DeletedField;
            }
            set {
                if ((this.DeletedField.Equals(value) != true)) {
                    this.DeletedField = value;
                    this.RaisePropertyChanged("Deleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime EndTime {
            get {
                return this.EndTimeField;
            }
            set {
                if ((this.EndTimeField.Equals(value) != true)) {
                    this.EndTimeField = value;
                    this.RaisePropertyChanged("EndTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIdField, value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BookAnArtisanMVC.RentingServiceReference.Material item {
            get {
                return this.itemField;
            }
            set {
                if ((object.ReferenceEquals(this.itemField, value) != true)) {
                    this.itemField = value;
                    this.RaisePropertyChanged("item");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Material", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Material : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AvailableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConditionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OwnerIdField;
        
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
        public bool Available {
            get {
                return this.AvailableField;
            }
            set {
                if ((this.AvailableField.Equals(value) != true)) {
                    this.AvailableField = value;
                    this.RaisePropertyChanged("Available");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Condition {
            get {
                return this.ConditionField;
            }
            set {
                if ((object.ReferenceEquals(this.ConditionField, value) != true)) {
                    this.ConditionField = value;
                    this.RaisePropertyChanged("Condition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted {
            get {
                return this.DeletedField;
            }
            set {
                if ((this.DeletedField.Equals(value) != true)) {
                    this.DeletedField = value;
                    this.RaisePropertyChanged("Deleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OwnerId {
            get {
                return this.OwnerIdField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnerIdField, value) != true)) {
                    this.OwnerIdField = value;
                    this.RaisePropertyChanged("OwnerId");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RentingServiceReference.IRentingService")]
    public interface IRentingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Create", ReplyAction="http://tempuri.org/IServiceOf_Rented/CreateResponse")]
        BookAnArtisanMVC.RentingServiceReference.Rented Create(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Create", ReplyAction="http://tempuri.org/IServiceOf_Rented/CreateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> CreateAsync(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Read", ReplyAction="http://tempuri.org/IServiceOf_Rented/ReadResponse")]
        BookAnArtisanMVC.RentingServiceReference.Rented Read(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Read", ReplyAction="http://tempuri.org/IServiceOf_Rented/ReadResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> ReadAsync(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Update", ReplyAction="http://tempuri.org/IServiceOf_Rented/UpdateResponse")]
        BookAnArtisanMVC.RentingServiceReference.Rented Update(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Update", ReplyAction="http://tempuri.org/IServiceOf_Rented/UpdateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> UpdateAsync(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Delete", ReplyAction="http://tempuri.org/IServiceOf_Rented/DeleteResponse")]
        BookAnArtisanMVC.RentingServiceReference.Rented Delete(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/Delete", ReplyAction="http://tempuri.org/IServiceOf_Rented/DeleteResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> DeleteAsync(BookAnArtisanMVC.RentingServiceReference.Rented t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Rented/ReadAllResponse")]
        BookAnArtisanMVC.RentingServiceReference.Rented[] ReadAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Rented/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Rented/ReadAllResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented[]> ReadAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRentingServiceChannel : BookAnArtisanMVC.RentingServiceReference.IRentingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RentingServiceClient : System.ServiceModel.ClientBase<BookAnArtisanMVC.RentingServiceReference.IRentingService>, BookAnArtisanMVC.RentingServiceReference.IRentingService {
        
        public RentingServiceClient() {
        }
        
        public RentingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RentingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RentingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RentingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BookAnArtisanMVC.RentingServiceReference.Rented Create(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.Create(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> CreateAsync(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.CreateAsync(t);
        }
        
        public BookAnArtisanMVC.RentingServiceReference.Rented Read(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.Read(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> ReadAsync(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.ReadAsync(t);
        }
        
        public BookAnArtisanMVC.RentingServiceReference.Rented Update(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.Update(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> UpdateAsync(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.UpdateAsync(t);
        }
        
        public BookAnArtisanMVC.RentingServiceReference.Rented Delete(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.Delete(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented> DeleteAsync(BookAnArtisanMVC.RentingServiceReference.Rented t) {
            return base.Channel.DeleteAsync(t);
        }
        
        public BookAnArtisanMVC.RentingServiceReference.Rented[] ReadAll() {
            return base.Channel.ReadAll();
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.RentingServiceReference.Rented[]> ReadAllAsync() {
            return base.Channel.ReadAllAsync();
        }
    }
}

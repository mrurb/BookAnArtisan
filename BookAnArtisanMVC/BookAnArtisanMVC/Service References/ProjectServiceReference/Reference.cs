﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookAnArtisanMVC.ProjectServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Project", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Project : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BookAnArtisanMVC.ProjectServiceReference.User ContactField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BookAnArtisanMVC.ProjectServiceReference.User CreatedByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProjectDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ProjectStatusIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreetNameField;
        
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
        public BookAnArtisanMVC.ProjectServiceReference.User Contact {
            get {
                return this.ContactField;
            }
            set {
                if ((object.ReferenceEquals(this.ContactField, value) != true)) {
                    this.ContactField = value;
                    this.RaisePropertyChanged("Contact");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Created {
            get {
                return this.CreatedField;
            }
            set {
                if ((this.CreatedField.Equals(value) != true)) {
                    this.CreatedField = value;
                    this.RaisePropertyChanged("Created");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BookAnArtisanMVC.ProjectServiceReference.User CreatedBy {
            get {
                return this.CreatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedByField, value) != true)) {
                    this.CreatedByField = value;
                    this.RaisePropertyChanged("CreatedBy");
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
        public System.DateTime Modified {
            get {
                return this.ModifiedField;
            }
            set {
                if ((this.ModifiedField.Equals(value) != true)) {
                    this.ModifiedField = value;
                    this.RaisePropertyChanged("Modified");
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
        public string ProjectDescription {
            get {
                return this.ProjectDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.ProjectDescriptionField, value) != true)) {
                    this.ProjectDescriptionField = value;
                    this.RaisePropertyChanged("ProjectDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProjectStatusID {
            get {
                return this.ProjectStatusIDField;
            }
            set {
                if ((this.ProjectStatusIDField.Equals(value) != true)) {
                    this.ProjectStatusIDField = value;
                    this.RaisePropertyChanged("ProjectStatusID");
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
        public string StreetName {
            get {
                return this.StreetNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StreetNameField, value) != true)) {
                    this.StreetNameField = value;
                    this.RaisePropertyChanged("StreetName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AccessFailedCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ApiKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool EmailConfirmedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool LockoutEnabledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LockoutEndDateUtcField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordHashField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PhoneNumberConfirmedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SecurityStampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool TwoFactorEnabledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
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
        public int AccessFailedCount {
            get {
                return this.AccessFailedCountField;
            }
            set {
                if ((this.AccessFailedCountField.Equals(value) != true)) {
                    this.AccessFailedCountField = value;
                    this.RaisePropertyChanged("AccessFailedCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ApiKey {
            get {
                return this.ApiKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.ApiKeyField, value) != true)) {
                    this.ApiKeyField = value;
                    this.RaisePropertyChanged("ApiKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool EmailConfirmed {
            get {
                return this.EmailConfirmedField;
            }
            set {
                if ((this.EmailConfirmedField.Equals(value) != true)) {
                    this.EmailConfirmedField = value;
                    this.RaisePropertyChanged("EmailConfirmed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool LockoutEnabled {
            get {
                return this.LockoutEnabledField;
            }
            set {
                if ((this.LockoutEnabledField.Equals(value) != true)) {
                    this.LockoutEnabledField = value;
                    this.RaisePropertyChanged("LockoutEnabled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LockoutEndDateUtc {
            get {
                return this.LockoutEndDateUtcField;
            }
            set {
                if ((this.LockoutEndDateUtcField.Equals(value) != true)) {
                    this.LockoutEndDateUtcField = value;
                    this.RaisePropertyChanged("LockoutEndDateUtc");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordHash {
            get {
                return this.PasswordHashField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordHashField, value) != true)) {
                    this.PasswordHashField = value;
                    this.RaisePropertyChanged("PasswordHash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool PhoneNumberConfirmed {
            get {
                return this.PhoneNumberConfirmedField;
            }
            set {
                if ((this.PhoneNumberConfirmedField.Equals(value) != true)) {
                    this.PhoneNumberConfirmedField = value;
                    this.RaisePropertyChanged("PhoneNumberConfirmed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecurityStamp {
            get {
                return this.SecurityStampField;
            }
            set {
                if ((object.ReferenceEquals(this.SecurityStampField, value) != true)) {
                    this.SecurityStampField = value;
                    this.RaisePropertyChanged("SecurityStamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool TwoFactorEnabled {
            get {
                return this.TwoFactorEnabledField;
            }
            set {
                if ((this.TwoFactorEnabledField.Equals(value) != true)) {
                    this.TwoFactorEnabledField = value;
                    this.RaisePropertyChanged("TwoFactorEnabled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProjectServiceReference.IProjectService")]
    public interface IProjectService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Create", ReplyAction="http://tempuri.org/IServiceOf_Project/CreateResponse")]
        BookAnArtisanMVC.ProjectServiceReference.Project Create(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Create", ReplyAction="http://tempuri.org/IServiceOf_Project/CreateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> CreateAsync(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Read", ReplyAction="http://tempuri.org/IServiceOf_Project/ReadResponse")]
        BookAnArtisanMVC.ProjectServiceReference.Project Read(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Read", ReplyAction="http://tempuri.org/IServiceOf_Project/ReadResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> ReadAsync(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Update", ReplyAction="http://tempuri.org/IServiceOf_Project/UpdateResponse")]
        BookAnArtisanMVC.ProjectServiceReference.Project Update(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Update", ReplyAction="http://tempuri.org/IServiceOf_Project/UpdateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> UpdateAsync(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Delete", ReplyAction="http://tempuri.org/IServiceOf_Project/DeleteResponse")]
        BookAnArtisanMVC.ProjectServiceReference.Project Delete(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/Delete", ReplyAction="http://tempuri.org/IServiceOf_Project/DeleteResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> DeleteAsync(BookAnArtisanMVC.ProjectServiceReference.Project t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Project/ReadAllResponse")]
        System.Collections.Generic.List<BookAnArtisanMVC.ProjectServiceReference.Project> ReadAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Project/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Project/ReadAllResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BookAnArtisanMVC.ProjectServiceReference.Project>> ReadAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProjectServiceChannel : BookAnArtisanMVC.ProjectServiceReference.IProjectService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProjectServiceClient : System.ServiceModel.ClientBase<BookAnArtisanMVC.ProjectServiceReference.IProjectService>, BookAnArtisanMVC.ProjectServiceReference.IProjectService {
        
        public ProjectServiceClient() {
        }
        
        public ProjectServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProjectServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProjectServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProjectServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BookAnArtisanMVC.ProjectServiceReference.Project Create(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.Create(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> CreateAsync(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.CreateAsync(t);
        }
        
        public BookAnArtisanMVC.ProjectServiceReference.Project Read(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.Read(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> ReadAsync(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.ReadAsync(t);
        }
        
        public BookAnArtisanMVC.ProjectServiceReference.Project Update(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.Update(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> UpdateAsync(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.UpdateAsync(t);
        }
        
        public BookAnArtisanMVC.ProjectServiceReference.Project Delete(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.Delete(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.ProjectServiceReference.Project> DeleteAsync(BookAnArtisanMVC.ProjectServiceReference.Project t) {
            return base.Channel.DeleteAsync(t);
        }
        
        public System.Collections.Generic.List<BookAnArtisanMVC.ProjectServiceReference.Project> ReadAll() {
            return base.Channel.ReadAll();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BookAnArtisanMVC.ProjectServiceReference.Project>> ReadAllAsync() {
            return base.Channel.ReadAllAsync();
        }
    }
}

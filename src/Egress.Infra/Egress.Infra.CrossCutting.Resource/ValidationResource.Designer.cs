﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Egress.Infra.CrossCutting.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Egress.Infra.CrossCutting.Resource.ValidationResource", typeof(ValidationResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0005] {0} contains unsupported format{1}.
        /// </summary>
        public static string VALIDATION_CONTAINS_UNSUPPORTED_FORMAT {
            get {
                return ResourceManager.GetString("VALIDATION_CONTAINS_UNSUPPORTED_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0009] Course parameter needs to be empty for this PersonType.
        /// </summary>
        public static string VALIDATION_COURSE_MUST_BE_EMPTY {
            get {
                return ResourceManager.GetString("VALIDATION_COURSE_MUST_BE_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0003] {PropertyName} must be greater than {ComparisonValue}.
        /// </summary>
        public static string VALIDATION_GREATER_THEN_OR_EQUAL {
            get {
                return ResourceManager.GetString("VALIDATION_GREATER_THEN_OR_EQUAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0004] {0} contains invalid format{1}.
        /// </summary>
        public static string VALIDATION_INVALID_FORMAT {
            get {
                return ResourceManager.GetString("VALIDATION_INVALID_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0007] {PropertyName} is invalid.
        /// </summary>
        public static string VALIDATION_IS_INVALID {
            get {
                return ResourceManager.GetString("VALIDATION_IS_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0006] {0} is limited to {1}.
        /// </summary>
        public static string VALIDATION_IS_LIMITED_TO {
            get {
                return ResourceManager.GetString("VALIDATION_IS_LIMITED_TO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0008] {0} is required.
        /// </summary>
        public static string VALIDATION_IS_REQUIRED {
            get {
                return ResourceManager.GetString("VALIDATION_IS_REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0001] {PropertyName} cannot be empty.
        /// </summary>
        public static string VALIDATION_NOT_EMPTY {
            get {
                return ResourceManager.GetString("VALIDATION_NOT_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [VA0002] {PropertyName} cannot be null.
        /// </summary>
        public static string VALIDATION_NOT_NULL {
            get {
                return ResourceManager.GetString("VALIDATION_NOT_NULL", resourceCulture);
            }
        }
    }
}

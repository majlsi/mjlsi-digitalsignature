﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.GlobalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SystemMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SystemMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebAPI.GlobalResources.SystemMessages", typeof(SystemMessages).Assembly);
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
        ///   Looks up a localized string similar to لا يمكن تعديل الملف لان به توقيعات.
        /// </summary>
        public static string AlreadySigned {
            get {
                return ResourceManager.GetString("AlreadySigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رفض.
        /// </summary>
        public static string Cancel {
            get {
                return ResourceManager.GetString("Cancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FieldTypeId غير موجود .
        /// </summary>
        public static string FieldTypesNotFound {
            get {
                return ResourceManager.GetString("FieldTypesNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الملف غير موجود.
        /// </summary>
        public static string FileNotExist {
            get {
                return ResourceManager.GetString("FileNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الملف يجب أن يكون pdf.
        /// </summary>
        public static string FilePDF {
            get {
                return ResourceManager.GetString("FilePDF", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لم يوقع بعد.
        /// </summary>
        public static string NotSignedYet {
            get {
                return ResourceManager.GetString("NotSignedYet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to وقع.
        /// </summary>
        public static string Sign {
            get {
                return ResourceManager.GetString("Sign", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رفض التوقيع.
        /// </summary>
        public static string SignRefused {
            get {
                return ResourceManager.GetString("SignRefused", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حدث خطأ عند رفع  الملف.
        /// </summary>
        public static string UploadFailed {
            get {
                return ResourceManager.GetString("UploadFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to البريد الالكتروني للمستخدم غير موجود .
        /// </summary>
        public static string UserEmailsNotFound {
            get {
                return ResourceManager.GetString("UserEmailsNotFound", resourceCulture);
            }
        }
    }
}

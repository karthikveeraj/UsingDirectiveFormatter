﻿namespace UsingDirectiveFormatter.Commands
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(FormatCommandPackage.PackageGuidString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(FormatOptionGrid), "Using Directive Formatter 2", "Options", 0, 0, true)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class FormatCommandPackage : AsyncPackage
    {
        /// <summary>
        /// FormatCommandPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "a385812e-17cc-4ed0-a6d0-f0d8db1ff294";

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatCommand"/> class.
        /// </summary>
        public FormatCommandPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        //
        // Summary:
        //     The async initialization portion of the package initialization process. This
        //     method is invoked from a background thread.
        //
        // Parameters:
        //   cancellationToken:
        //     A cancellation token to monitor for initialization cancellation, which can occur
        //     when VS is shutting down.
        //
        // Returns:
        //     A task representing the async work of package initialization, or an already completed
        //     task if there is none. Do not return null from this method.
        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            FormatCommand.Initialize(this);
            await base.InitializeAsync(cancellationToken, progress);
        }

        #endregion Package Members

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <returns></returns>
        internal FormatOptionGrid GetOptions()
        {
            return (FormatOptionGrid)GetDialogPage(typeof(FormatOptionGrid));
        }
    }
}
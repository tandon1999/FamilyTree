namespace FamilyTree_UI.Shared.Models
{
    public class ConfirmDialogOptions
    {
        /// <summary>
        /// Shows the confirm dialog vertically in the center of the page.
        /// </summary>
        public bool IsVerticallyCentered { get; set; } = false;

        /// <summary>
        /// Gets or sets the 'Yes' button text.
        /// </summary>
        public string YesButtonText { get; set; } = "Yes";

        /// <summary>
        /// Gets or sets the 'Yes' button color.
        /// </summary>
        public ButtonColor YesButtonColor { get; set; } = ButtonColor.Primary;

        /// <summary>
        /// Gets or sets the 'No' button text.
        /// </summary>
        public string NoButtonText { get; set; } = "No";

        /// <summary>
        /// Gets or sets the 'No' button color.
        /// </summary>
        public ButtonColor NoButtonColor { get; set; } = ButtonColor.Secondary;

        /// <summary>
        /// Allows confirm dialog body to be scrollable.
        /// </summary>
        public bool IsScrollable { get; set; } = false;

        /// <summary>
        /// Adds a dismissable close button to the confirm dialog.
        /// </summary>
        public bool Dismissable { get; set; } = true;

        /// <summary>
        /// Additional CSS class for the dialog.
        /// </summary>
        public string? DialogCssClass { get; set; }

        /// <summary>
        /// Additional CSS class for the header.
        /// </summary>
        public string? HeaderCssClass { get; set; }

        /// <summary>
        /// Size of the modal (Small, Regular, Large, ExtraLarge).
        /// </summary>
        public DialogSize Size { get; set; } = DialogSize.Regular;
    }

    public enum ButtonColor
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    public enum DialogSize
    {
        Small,
        Regular,
        Large,
        ExtraLarge
    }
}

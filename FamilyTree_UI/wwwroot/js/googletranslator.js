function googleTranslateElementInit() {
    new google.translate.TranslateElement({
        pageLanguage: 'ne',
        includedLanguages: 'en,ne',
        layout: google.translate.TranslateElement.InlineLayout.SIMPLE
    }, 'google_translate_element');
}

function includeNepaliDateLibrary() {
    if (typeof NepaliFunctions === 'undefined') {
        var script = document.createElement('script');
        script.src = 'https://unpkg.com/nepali-date-converter/build/js/nepali-date-converter.min.js';
        document.head.appendChild(script);
    }
}

function convertToNepaliDate(englishDateString) {

    if (typeof NepaliFunctions === 'undefined') {
        return "Library not loaded yet.";
    }

    try {
        const englishDate = new Date(englishDateString);
        const nepaliDate = NepaliFunctions.AD2BS(englishDate.getFullYear(), englishDate.getMonth() + 1, englishDate.getDate());
        return nepaliDate.year + "-" + (nepaliDate.month < 10 ? '0' + nepaliDate.month : nepaliDate.month) + "-" + (nepaliDate.day < 10 ? '0' + nepaliDate.day : nepaliDate.day);
    } catch (error) {
        return "Invalid date or conversion error.";
    }
}
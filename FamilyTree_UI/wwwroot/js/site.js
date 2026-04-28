window.scrollToTop = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};

window.scrollToElement = function (elementId) {
    const target = document.getElementById(elementId);
    if (!target) {
        return;
    }

    target.scrollIntoView({ behavior: 'smooth', block: 'start' });
};
window.onscroll = function () {
    const button = document.querySelector('.back-to-top');
    if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
        button.style.display = "block";
    } else {
        button.style.display = "none";
    }
};

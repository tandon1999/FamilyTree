window.scrollToTop = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};

window.scrollToElement = function (elementId) {
    const target = document.getElementById(elementId);
    if (!target) {
        return;
    }

    const header = document.querySelector('.custom-navbar');
    const offset = header ? header.offsetHeight + 12 : 0;
    const top = target.getBoundingClientRect().top + window.pageYOffset - offset;

    window.scrollTo({ top: Math.max(top, 0), behavior: 'smooth' });
};
window.onscroll = function () {
    const button = document.querySelector('.back-to-top');
    if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
        button.style.display = "block";
    } else {
        button.style.display = "none";
    }
};

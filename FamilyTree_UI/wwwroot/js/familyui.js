window.getElementPosition = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        const rect = element.getBoundingClientRect();
        return { x: rect.left + window.scrollX, y: rect.top + window.scrollY };
    }
    return { x: 0, y: 0 };
};

window.drawConnectorLine = (elementId, x, y, length, angle) => {
    const element = document.getElementById(elementId);
    if (element) {
        element.style.left = `${x}px`;
        element.style.top = `${y}px`;
        element.style.width = `${length}px`;
        element.style.transform = `rotate(${angle}deg)`;
    }
};

window.getConnectorLine = (elementId) => {
    return document.getElementById(elementId);
};
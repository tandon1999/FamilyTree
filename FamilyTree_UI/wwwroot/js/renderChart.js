/*function renderChart(config) {
    var ctx = document.getElementById('myChart').getContext('2d');
    new Chart(ctx, config);
}*/

/*window.renderChart = (canvasId, config) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, config);
};*/

// Global chart instance store

window.chartInstances = window.chartInstances || {};

window.renderChart = (canvasId, config) => {
    const ctx = document.getElementById(canvasId).getContext("2d");

    // 🔥 Destroy existing chart with same canvasId if it exists
    if (window.chartInstances[canvasId]) {
        window.chartInstances[canvasId].destroy();
    }

    const chart = new Chart(ctx, config);

    // 💾 Save chart instance to destroy later if needed
    window.chartInstances[canvasId] = chart;
};


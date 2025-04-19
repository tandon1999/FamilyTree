/*function renderChart(config) {
    var ctx = document.getElementById('myChart').getContext('2d');
    new Chart(ctx, config);
}*/

window.renderChart = (canvasId, config) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, config);
};

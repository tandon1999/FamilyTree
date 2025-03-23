function renderChart(config) {
    var ctx = document.getElementById('myChart').getContext('2d');
    new Chart(ctx, config);
}
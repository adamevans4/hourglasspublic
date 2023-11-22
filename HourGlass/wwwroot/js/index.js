// Bar chart data with categories
var barChartData = [
    { category: "Fitness", hours: 10 },
    { category: "School", hours: 20 },
    { category: "Gym", hours: 30 },
    { category: "Programming", hours: 40 },
    { category: "Sleep", hours: 50 }
];

var barChartContainerWidth = 300; // Adjust the desired width
var barChartContainerHeight = 300; // Adjust the desired height

var barChartSvg = d3.select("#bar-chart-container")
    .append("svg")
    .attr("width", barChartContainerWidth)
    .attr("height", barChartContainerHeight)
    .append("g")
    .attr("transform", "translate(" + (barChartContainerWidth / 4) + "," + (barChartContainerHeight / 20) + ")");

// Set up X and Y scales
var xScale = d3.scaleBand()
    .domain(barChartData.map(function (d) { return d.category; }))
    .range([0, 200])
    .padding(0.1);

var yScale = d3.scaleLinear()
    .domain([0, d3.max(barChartData, function (d) { return d.hours; })])
    .range([0, 150]);

// Create X axis with rotated labels
barChartSvg.append("g")
    .attr("transform", "translate(0,150)")
    .call(d3.axisBottom(xScale))
    .selectAll("text")
    .style("text-anchor", "end")
    .attr("dx", "-.8em")
    .attr("dy", ".15em")
    .attr("transform", "rotate(-30)");

// Create Y axis
barChartSvg.append("g")
    .call(d3.axisLeft(yScale));

// Add X axis label
barChartSvg.append("text")
    .attr("transform", "translate(150,200)")
    .style("text-anchor", "middle")
    .text("Activities");

// Add Y axis label
barChartSvg.append("text")
    .attr("transform", "rotate(-90)")
    .attr("y", 0 - 40)
    .attr("x", 0 - 75)
    .attr("dy", "1em")
    .style("text-anchor", "middle")
    .text("Hours");

// Create bars for the bar chart
barChartSvg.selectAll("rect")
    .data(barChartData)
    .enter()
    .append("rect")
    .attr("x", function (d) { return xScale(d.category); })
    .attr("y", function (d) { return 150 - yScale(d.hours); })
    .attr("width", xScale.bandwidth())
    .attr("height", function (d) { return yScale(d.hours); })
    .attr("fill", "blue");

// Calculate the total sum of hours
var totalHours = barChartData.reduce(function (sum, data) {
    return sum + data.hours;
}, 0);

// Convert hours to percentages
barChartData.forEach(function (data) {
    data.percentage = (data.hours / totalHours) * 100;
});

// Set up the SVG container for the pie chart
var pieChartSvg = d3.select("#pie-chart-container")
    .append("svg")
    .attr("width", 200)
    .attr("height", 150)
    .append("g")
    .attr("transform", "translate(100,75)");

// Pie chart data using the percentages calculated from the bar chart data
var pieChartData = barChartData.map(function (data) {
    return data.percentage;
});

// Create a pie chart
var pie = d3.pie();
var arc = d3.arc().innerRadius(0).outerRadius(75);

pieChartSvg.selectAll("path")
    .data(pie(pieChartData))
    .enter()
    .append("path")
    .attr("d", arc)
    .attr("fill", function (d, i) { return d3.schemeCategory10[i]; });

// Display percentages and rank in the div
var weeklyStatsDiv = d3.select("#weekly-stats");

barChartData.sort(function (a, b) {
    return b.hours - a.hours;
});

barChartData.forEach(function (data, index) {
    weeklyStatsDiv.append("p")
        .text((index + 1) + ". " + data.category + ": " + data.percentage.toFixed(2) + "%")
        .style("font-size", "24px"); // Adjust the font size as needed
});
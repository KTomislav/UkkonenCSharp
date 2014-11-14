window.onload = function() {
var g = new Graph();
var width = $(document).width() - 20;
var height = $(document).height() - 60;
g.addEdge("0", "1", {label: "abc", directed:true});
g.addEdge("1", "#1", {label: "abx", directed:true});
g.addEdge("1", "#1", {label: "x", directed:true});
g.addEdge("1", "2", {directed:true, stroke: "#FF0000"});
g.addEdge("0", "2", {label: "bc", directed:true});
g.addEdge("2", "#2", {label: "abx", directed:true});
g.addEdge("2", "#2", {label: "x", directed:true});
g.addEdge("0", "#0", {label: "cabx", directed:true});
g.addEdge("0", "#0", {label: "x", directed:true});
var layouter = new Graph.Layout.Spring(g);
layouter.layout();
var renderer = new Graph.Renderer.Raphael('canvas', g, width, height);
renderer.draw();}


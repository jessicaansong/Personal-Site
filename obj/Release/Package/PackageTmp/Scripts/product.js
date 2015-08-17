// JavaScript source code
function productOfFive() {
    var n1 = document.getElementById('product_n1').value;
    var n2 = document.getElementById('product_n2').value;
    var n3 = document.getElementById('product_n3').value;
    var n4 = document.getElementById('product_n4').value;
    var n5 = document.getElementById('product_n5').value;
    var result = n1 * n2;
    result = result * n3;
    result = result * n4;
    result = result * n5;
    var display = document.getElementById('product');
    display.innerHTML = "The product is " + result;
}
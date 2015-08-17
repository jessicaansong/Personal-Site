// JavaScript source code
function maxOfFive(){
    var n1 = document.getElementById('n1').value;
    var n2 = document.getElementById('n2').value;
    var n3 = document.getElementById('n3').value;
    var n4 = document.getElementById('n4').value;
    var n5 = document.getElementById('n5').value;
    var result = Math.max(n1, n2, n3, n4, n5);
    var display = document.getElementById('maxResult');
    display.innerHTML = "The largest number is " + result ;
    }

    
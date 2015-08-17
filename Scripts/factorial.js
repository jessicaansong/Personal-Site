// JavaScript source code
function fact() {
    var n1 = parseInt(document.getElementById('fact_1').value);//text box to accept numbers. '.value takes the value of the number.
    
    var result = calculate(n1)
    var display = document.getElementById('factResult');
    display.innerHTML = "The factorial of the number is " + result ;
}
function calculate(n) {
    var res = 1;
    for (i=n; i>=1; i--)
        res = res*i;
    return res; 
   }    
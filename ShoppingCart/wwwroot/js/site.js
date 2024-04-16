const logoutDiv = document.getElementById("logout-div");
const logioresult = document.getElementById("result");
window.onload = function () {
    
    // Login logic
    if (window.location.href.includes("/login")) {
        const loginEl = document.getElementById("login-form");
        
        if (loginEl != null && loginEl.onsubmit == null) {
           nt.target.password.value
               
        }
    } else {
        
        logoutDiv.style.display = "block";
    }

}

function resetLogIOResult() {
    logioresult.innerHTML = "";
}

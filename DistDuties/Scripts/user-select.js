var userEmailInput = document.getElementById("userEmail");
document.getElementById("user-list").addEventListener("click", function (e) {
    if (e.target && e.target.matches("li")) {
        userEmailInput.value = e.target.textContent;
    }
});
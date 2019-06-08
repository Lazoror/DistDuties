var sidebar = document.getElementById("users-sidebar");
var navHeight = document.getElementById("nav").offsetHeight;
var projectInfo = document.getElementById("project-info");
var usersList = document.getElementById("user-list");

//document.body.onscroll = function () {
//    if (scrollY > navHeight) {
//        sidebar.setAttribute("style", "height: 100vh");
//        projectInfo.setAttribute("style", "z-index: 1999; background-color: #FFEB00;");
//        usersList.setAttribute("style", "height: calc(100vh - 170px);");

//    }
//    if (scrollY < navHeight) {
//        projectInfo.setAttribute("style", "z-index: 1999");
//        usersList.setAttribute("style", "height: calc(100vh - 225px);");
        
//    }
//}

function collapseUsers() {
    var sideb = document.querySelector("#sidebar");
    var userControls = document.querySelector(".user-controls");
    var mainControls = document.querySelector("#main-controls");

    if (sideb.classList.contains("col-md-2")) {
        sideb.classList.remove("col-md-2");
        sideb.classList.add("col-md-1");
        userControls.classList.add("hidden");
        mainControls.classList.remove("col-md-10");
        mainControls.classList.add("col-md-11");

    } else {
        sideb.classList.remove("col-md-1");
        sideb.classList.add("col-md-2");
        userControls.classList.remove("hidden");
        mainControls.classList.remove("col-md-11");
        mainControls.classList.add("col-md-10");
    }

    
}

document.querySelector("#collapse-users").addEventListener("click", function (e) {
    collapseUsers();
});
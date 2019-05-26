var sidebar = document.getElementById("users-sidebar");
var navHeight = document.getElementById("nav").offsetHeight;
var projectInfo = document.getElementById("project-info");
var usersList = document.getElementById("user-list");

document.body.onscroll = function () {
    if (scrollY > navHeight) {
        sidebar.setAttribute("style", "height: 100vh");
        projectInfo.setAttribute("style", "z-index: 1999; background-color: #FFEB00;");
        usersList.setAttribute("style", "height: calc(100vh - 170px);");

    }
    if (scrollY < navHeight) {
        projectInfo.setAttribute("style", "z-index: 1999");
        usersList.setAttribute("style", "height: calc(100vh - 225px);");
        
    }
}
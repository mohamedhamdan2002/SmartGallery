let searchBtn = document.querySelector("#search-btn");
let searchForm = document.querySelector(".search-form");
let logInForm = document.querySelector(".login-form");
let menuBar = document.querySelector("#menu-bar");
let amenu = document.querySelector(".navbar");
let videobtn = document.querySelectorAll(".video-btn");
function showbar() {
    searchBtn.classList.toggle("fa-times");
    searchForm.classList.toggle("active");
}
function loginform() {
    logInForm.classList.add("active");
}
function hideform() {
    logInForm.classList.remove("active");
}
function showmenu() {
    menuBar.classList.toggle("fa-times");

    amenu.classList.toggle("active");
}
videobtn.forEach((slide) => {
    slide.addEventListener("click", function () {
        document.querySelector(".controls .blue").classList.remove("blue");
        slide.classList.add("blue");
        let src = slide.getAttribute("data-src");
        document.querySelector("#video-slider").src = src;
    });
});
window.scrollToSection = function (sectionId) {
    var element = document.getElementById(sectionId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
}
function initSwiper() {
    var swiper = new Swiper(".review-slider", {
        spaceetween: 20,
        loop: true,
        autoplay: {
            delay: 2500,
        },
        breakpoints: {
            640: {
                slidesPerView: 1,
            },
            770: {
                slidesPerView: 2,
            },
            1070: {
                slidesPerView: 3,
            },
        },
    });
}

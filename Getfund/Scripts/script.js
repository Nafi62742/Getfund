const imgs = document.querySelectorAll('.img-select a');
const imgBtns = [...imgs];
let imgId = 1;
var toggle_btn;
var big_wrapper;
var hamburger_menu;

function declare() {
    toggle_btn = document.querySelector(".toggle-btn");
    big_wrapper = document.querySelector(".big-wrapper");
    hamburger_menu = document.querySelector(".hamburger-menu");
}

const main = document.querySelector("main");

declare();

imgBtns.forEach((imgItem) => {
    imgItem.addEventListener('click', (event) => {
        event.preventDefault();
        imgId = imgItem.dataset.id;
        slideImage();
    });
});

function slideImage() {
    const displayWidth = document.querySelector('.img-showcase img:first-child').clientWidth;

    document.querySelector('.img-showcase').style.transform = `translateX(${- (imgId - 1) * displayWidth}px)`;
}

window.addEventListener('resize', slideImage);

function events() {
    toggle_btn.addEventListener("click", toggleAnimation);
    hamburger_menu.addEventListener("click", () => {
        big_wrapper.classList.toggle("active");
    });
}

events();


    $(document).ready(function(){
        $('.content').click(function () {
            $('.content').toggleClass("heart-active")
            $('.text').toggleClass("heart-active")
            $('.numb').toggleClass("heart-active")
            $('.heart').toggleClass("heart-active")
        });
      });

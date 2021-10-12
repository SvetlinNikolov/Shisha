// Products JS logic
// Variable definitions
let allImageNavButtons = document.querySelectorAll('.product-image-and-text-container .product-image-container .product-image-navigation-container button');
let allPackagingButtons = document.querySelectorAll('.product-image-and-text-container .price-and-button-container .packaging-choices-container button');

// Function definitions
function changeBigImage() {
    // Get elements
    let currentImageBtn = document.querySelector('.product-image-and-text-container .product-image-container .product-image-navigation-container button.active');
    let newImageBtn = this;
    let newImageButtonsrc = newImageBtn.firstElementChild.src;
    let bigImage = document.querySelector('.product-image-and-text-container .product-image-container .product-big-image');

    // Set 
    currentImageBtn.classList.remove('active');
    newImageBtn.classList.add('active');
    bigImage.src = newImageButtonsrc;
}

function changePackaging() {
    // Get elements6
    let currentPackaging = document.querySelector('.product-image-and-text-container .price-and-button-container .packaging-choices-container .packaging-choice.active');
    let newPackaging = this;

    // Set 
    currentPackaging.classList.remove('active');
    newPackaging.classList.add('active');
}

// Add event listeners
// Image navigation
for (let ImageBtn of allImageNavButtons) {
    ImageBtn.addEventListener('click', changeBigImage);
}

// Packaging selection
for (let packagingBtn of allPackagingButtons) {
    packagingBtn.addEventListener('click', changePackaging);
}

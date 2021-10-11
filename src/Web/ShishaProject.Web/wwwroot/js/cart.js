// Cart JS logic
// Variable definitions
let addToCartButtons = document.querySelectorAll('.product-add-to-cart-button');

// Function definitions
function addProductToCart() {
    let productParent = this.parentNode.attributes;
    let productAttributeObject = {};

    for (let attribute of productParent) {
        productAttributeObject[attribute.name] = attribute.value;
    }

    let productNumber = productAttributeObject['data-product-number'];

    // TO DO - SEND TO THE BE
    console.log('Product number: ', productNumber);
    // postRequest('addUrl', { data })
    // .then(data => {
    //     console.log(data);
    // });
    // TO DO - SEND TO THE BE
}

// Add event listeners
for (let button of addToCartButtons) {
    button.addEventListener('click', addProductToCart);
}

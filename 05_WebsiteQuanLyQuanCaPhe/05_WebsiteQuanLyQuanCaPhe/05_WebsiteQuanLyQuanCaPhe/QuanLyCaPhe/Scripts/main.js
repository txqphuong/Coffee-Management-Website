//background darker & open/close cart
var btnCart = document.getElementById("cart-icon");
var boxCart =  document.getElementById("myCart");;
var screenDarker = document.getElementById("darker-bg")
var btnCloseCart = document.getElementById("close-cart")
btnCart.addEventListener("click", ()=>{
    boxCart.style.transform = 'translateX(0)';
    screenDarker.style.opacity = '0.4';
    screenDarker.style.zIndex = '99';
})
btnCloseCart.addEventListener("click", ()=>{
    boxCart.style.transform = 'translateX(1000px)';
    screenDarker.style.opacity = '0';
    screenDarker.style.zIndex = '-1';
})
function updateTotal()
{
    var cartContent=document.getElementsByClassName("cart-content")[0];
    var cartBoxes=cartContent.getElementsByClassName("cart-box");
    var total=0
    for(var i=0;i<cartBoxes.length;i++){
        var cartBox=cartBoxes[i]
        var priceElement=cartBox.getElementsByClassName("cart-price")[0];
        var quantityElement=cartBox.getElementsByClassName("cart-quantity")[0];
        var price=parseFloat(priceElement.innerText);
        var quantity=quantityElement.value;
        total=total+(price*quantity);
        console.log(price)
        console.log(price)
        console.log(price)
}
//if price has a demical part 
total=Math.round(total*100)/100;

document.getElementsByClassName("total-price")[0].innerText=total+" VND";
}
//cart working
if(document.readyState=='loading'){
    document.addEventListener('DOMContentLoaded',ready);
}
else{
    ready();
}

//function
function ready(){
    //remove items from cart
    var removeCartButton=document.getElementsByClassName("cart-remove");
    console.log(removeCartButton);
    for(var i=0;i<removeCartButton.length;i++)
    {
        var button =removeCartButton[i];
        button.addEventListener("click", removeCartItem);
    }
}

//change the quantity
var quantityInputs=document.getElementsByClassName("cart-quantity");
for(var i=0;i<quantityInputs.length;i++)
{
    var input=quantityInputs[i];
    input.addEventListener("change", quantityChanged);
}
//add to cart
var addCart=document.getElementsByClassName("add-cart");
for(var i=0;i<addCart.length;i++){
    var button=addCart[i];
    button.addEventListener("click", addCartClicked);
}

//buy button
document.getElementsByClassName("btn-buy")[0].addEventListener("click",buybuttonClicked);


////button Buy Now
function buybuttonClicked(){
    alert("Your order is placed");
    var cartContent=document.getElementsByClassName("cart-content")[0];
    while(cartContent.hasChildNodes())
    {
        cartContent.removeChild(cartContent.firstChild);
    }
    updateTotal();
}

////remove item from cart
function removeCartItem (event){
var buttonClicked= event.target;
buttonClicked.parentElement.remove();
updateTotal();
}

function quantityChanged(event)
{
var input=event.target;
if(isNaN(input.value)||input.value<=0){
    input.value=1;
}
updateTotal();
}

function addCartClicked(event){
var button=event.target;
var shopProducts=button.parentElement;
var title=shopProducts.getElementsByClassName("product-title")[0].innerText;
var price=shopProducts.getElementsByClassName("price")[0].innerText;
var productImg=shopProducts.getElementsByClassName("product-img")[0].src;
addProductCart(title, price, productImg);
updateTotal();
}

function addProductCart(title,price,productImg){
    var cartShopBox=document.createElement("div");
    cartShopBox.classList.add("cart-box");
    var cartItems=document.getElementsByClassName("cart-content");
    var cartItemsNames=cartItems.getElementsByClassName("cart-product-title");
    for(var i=0; i<cartItemsNames.length;i++)
    {
        if(cartItemsNames[i].innerText==title)
        {
            alert("You already added this item to your cart");
            return;
        }
    }
    var cartBoxContent=`<img src="${productImg}" alt="" class="cart-img">
                        <div class="detail-box">
                        <div class="cart-product-title">
                            ${title}
                        </div>
                        <div class="cart-price">${price}</div>
                        <input type="number" class="cart-quantity" value="1"/>
                        </div>
                        <!--remove cart-->
                        <i class='bx bx-trash cart-remove'></i>`;
    cartShopBox.innerHTML=cartBoxContent
    cartItems.append(cartShopBox)
    cartShopBox.getElementsByClassName("cart-remove")[0].addEventListener("click", removeCartItem);
    cartShopBox.getElementsByClassName("cart-quantity")[0].addEventListener("change", quantityChanged);
}



//update total

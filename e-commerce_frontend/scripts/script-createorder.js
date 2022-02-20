const form = document.getElementById('createorderform')
const submit = document.getElementById('btn-submit')
const userid = document.getElementById('userid_input')
const productid = document.getElementById('productid_input')
const quantity = document.getElementById('quantity_input')
const productid2 = document.getElementById('productid2_input')
const quantity2 = document.getElementById('quantity2_input')
const productid3 = document.getElementById('productid3_input')
const quantity3 = document.getElementById('quantity3_input')
const orderstate = document.getElementById('createorderstate')
orderstate.value = ""

checkIfLoggedIn()

function postOrder(orderData) {
    fetch('https://localhost:7250/api/Orders?usercode=YXJlIHlvdSB1c2VyIG9yIG5vdA==', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${sessionStorage.getItem("accessToken")}`
        },
        body: orderData
    })
}

form.addEventListener('submit', function(e) {
    e.preventDefault()
    if(productid.value == "" || quantity.value == ""){
        productid.value = 0;
        quantity.value = 0;
    }

    if(productid2.value == "" || quantity2.value == ""){
        productid2.value = 0;
        quantity2.value = 0;
    }

    if(productid3.value == "" || quantity3.value == ""){
        productid3.value = 0;
        quantity3.value = 0;
    }
    let jsons = JSON.stringify({
        userId: userid.value, 
        orderLines: [
            {
                productId: productid.value,
                quantity: quantity.value
            },
            {
                productId: productid2.value,
                quantity: quantity2.value
            },
            {
                productId: productid3.value,
                quantity: quantity3.value
            }
        ]
    })
    postOrder(jsons)
    if(userid.value == "" || isNaN(userid.value)) {
        orderstate.value = "You need a valid User ID."
    } else {
        orderstate.value = "Order created succesfully."
    }
    userid.value = ""
    productid.value = ""
    quantity.value = ""
    productid2.value = ""
    quantity2.value = ""
    productid3.value = ""
    quantity3.value = ""
})

function checkIfLoggedIn() {
    if((`${sessionStorage.getItem("accessToken")}`).length < 40) {
        orderstate.value = "You need to log in."
    }
}
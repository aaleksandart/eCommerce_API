const form = document.getElementById('createproductform')
const submit = document.getElementById('btn-submit')
const productname = document.getElementById('productname_input')
const productdescription = document.getElementById('productdescription_input')
const price = document.getElementById('price_input')
const category = document.getElementById('categoryname_input')
const productstate = document.getElementById('createproductstate')
productstate.value = ""

checkIfLoggedIn()

function postProduct(productData) {
    fetch('https://localhost:7250/api/Products?usercode=YXJlIHlvdSB1c2VyIG9yIG5vdA==&admincode=YXJlIHlvdSBhZG1pbiBvciBub3Q=', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `bearer ${sessionStorage.getItem("accessToken-admin")}`
        },
        body: productData
    })
}

form.addEventListener('submit', function(e) {
    e.preventDefault()
    let jsons = JSON.stringify({
        productName: productname.value, 
        productDescription: productdescription.value, 
        price: price.value, 
        categoryName: category.value
    })
    postProduct(jsons)
    
    if(productname.value == "" || productdescription.value == "" || isNaN(price.value)|| category.value == "") {
        productstate.value = "You need to fill out all the info."
    } else {
        productstate.value = "Product created succesfully."
        productname.value = ""
        productdescription.value = ""
        price.value = ""
        category.value = ""
    }
})

function checkIfLoggedIn() {
    if((`${sessionStorage.getItem("accessToken-admin")}`).length < 50) {
        productstate.value = "You need to log in as a admin."
    }
}


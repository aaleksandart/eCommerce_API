const form = document.getElementById('form')
const submit = document.getElementById('btn-submit')
const firstname = document.getElementById('firstname_input')
const lastname = document.getElementById('lastname_input')
const email = document.getElementById('email_input')
const password = document.getElementById('password_input')
const phonenumber = document.getElementById('phonenumber_input')
const streetname = document.getElementById('streetname_input')
const postalcode = document.getElementById('postalcode_input')
const city = document.getElementById('city_input')
const country = document.getElementById('country_input')
const userstate = document.getElementById('createuserstate')
userstate.value = ""


const loginform = document.getElementById('login-form')
const login = document.getElementById('btn-login')
const loginemail = document.getElementById('loginemail_input')
const loginpassword = document.getElementById('loginpassword_input')



function postUser(userData) {
    fetch('https://localhost:7250/api/Users', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: userData
    })
}

form.addEventListener('submit', function(e) {
    e.preventDefault()
    let jsons = JSON.stringify({
        firstName: firstname.value, 
        lastName: lastname.value, 
        email: email.value, 
        password: password.value, 
        phoneNumber: phonenumber.value, 
        streetName: streetname.value, 
        postalCode: postalcode.value,
        city: city.value,
        country: country.value
    })
    postUser(jsons)
    if(firstname.value == "" || lastname.value == "" || email.value == "" || password.value == "" || phonenumber.value == "" || streetname.value == "" || postalcode.value == "" || city.value == "" || country.value == "") {
        userstate.value = "You need to fill everything out."
    }
    firstname.value = ""
    lastname.value = ""
    email.value = ""
    password.value = ""
    phonenumber.value = ""
    streetname.value = ""
    postalcode.value = ""
    city.value = ""
    country.value = ""
})
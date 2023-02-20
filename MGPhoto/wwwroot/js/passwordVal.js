const passwordInput = document.getElementById("psw");
// const rpasswordInput = document.getElementById("rpsw");
const letter = document.getElementById("letter");
const capital = document.getElementById("capital");
const number = document.getElementById("number");
const length = document.getElementById("length");
const invalid = document.getElementById("pattern");

// When the user clicks on the password field, show the message box
passwordInput.onfocus = function () {
    document.getElementById("message").style.display = "block";
};

// When the user clicks outside of the password field, hide the message box
passwordInput.onblur = function () {
    document.getElementById("message").style.display = "none";
};

// When the user starts to type something inside the password field
passwordInput.onkeyup = function () {
    // Validate lowercase letters
    validateLowercaseLetters(passwordInput);
    // Validate capital letters
    validateCapLetters(passwordInput);
    // Validate numbers
    validateNumbers(passwordInput);
    // Validate length
    validateLength(passwordInput);

    const patternText = "(?!.* )(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}";
    let regexPattern;


    try {
        regexPattern = new RegExp(patternText);
    } catch (err) {
        console.error(err);
    }

    const isFound = regexPattern.test(passwordInput.value);


};


function validateLowercaseLetters(passwordInput) {
    const lowerCaseLetters = /[a-z]/;
    if (passwordInput.value.match(lowerCaseLetters)) {
        letter.classList.remove("invalid");
        letter.classList.add("valid");
    } else {
        letter.classList.remove("valid");
        letter.classList.add("invalid");
    }
}

function validateCapLetters(passwordInput) {
    const upperCaseLetters = /[A-Z]/;
    if (passwordInput.value.match(upperCaseLetters)) {
        capital.classList.remove("invalid");
        capital.classList.add("valid");
    } else {
        capital.classList.remove("valid");
        capital.classList.add("invalid");
    }
}

function validateNumbers(passwordInput) {
    const numbers = /[0-9]/;
    if (passwordInput.value.match(numbers)) {
        number.classList.remove("invalid");
        number.classList.add("valid");
    } else {
        number.classList.remove("valid");
        number.classList.add("invalid");
    }
}

function validateLength(passwordInput) {
    if (passwordInput.value.length >= 8) {
        length.classList.remove("invalid");
        length.classList.add("valid");
    } else {
        length.classList.remove("valid");
        length.classList.add("invalid");
    }
}

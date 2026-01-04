// Telefon numarası formatlama
window.formatPhoneNumber = (input) => {
    let value = input.value.replace(/\D/g, ''); // Sadece rakamları al
    
    if (value.length === 0) {
        input.value = '';
        return;
    }
    
    // Türkiye telefon formatı: 0(5XX)XXX XX XX
    if (value.length <= 1) {
        input.value = value;
    } else if (value.length <= 4) {
        input.value = `0(${value.substring(1)}`;
    } else if (value.length <= 7) {
        input.value = `0(${value.substring(1, 4)})${value.substring(4)}`;
    } else if (value.length <= 9) {
        input.value = `0(${value.substring(1, 4)})${value.substring(4, 7)} ${value.substring(7)}`;
    } else {
        input.value = `0(${value.substring(1, 4)})${value.substring(4, 7)} ${value.substring(7, 9)} ${value.substring(9, 11)}`;
    }
};

// Öğrenci numarası formatlama
window.formatStudentNumber = (input) => {
    let value = input.value.replace(/\D/g, ''); // Sadece rakamları al
    
    if (value.length === 0) {
        input.value = '';
        return;
    }
    
    // Öğrenci numarası formatı: XXXX-XXXX veya benzer
    if (value.length <= 4) {
        input.value = value;
    } else {
        input.value = `${value.substring(0, 4)}-${value.substring(4, 8)}`;
    }
};


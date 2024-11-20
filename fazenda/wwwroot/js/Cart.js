// Função para adicionar itens ao carrinho
function addToCart(productName, price) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    const productIndex = cart.findIndex(item => item.name === productName);

    if (productIndex > -1) {
        cart[productIndex].quantity++;
    } else {
        cart.push({ name: productName, price: price, quantity: 1 });
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    updateCartDisplay();
}

// Função para atualizar a exibição do carrinho
function updateCartDisplay() {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const cartItemsContainer = document.getElementById('cart-items');
    cartItemsContainer.innerHTML = ''; // Limpar conteúdo

    let total = 0;

    cart.forEach(item => {
        total += item.price * item.quantity;

        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${item.name}</td>
            <td>R$ ${item.price.toFixed(2)}</td>
            <td>
                <button onclick="updateQuantity('${item.name}', -1)">-</button>
                ${item.quantity}
                <button onclick="updateQuantity('${item.name}', 1)">+</button>
            </td>
            <td>R$ ${(item.price * item.quantity).toFixed(2)}</td>
            <td><button onclick="removeFromCart('${item.name}')">Remover</button></td>
        `;
        cartItemsContainer.appendChild(row);
    });

    // Atualiza o total
    document.getElementById('cart-total').innerText = `R$ ${total.toFixed(2)}`;
}

// Função para atualizar a quantidade do item no carrinho
function updateQuantity(productName, change) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    const productIndex = cart.findIndex(item => item.name === productName);

    if (productIndex > -1) {
        cart[productIndex].quantity += change;
        if (cart[productIndex].quantity <= 0) {
            cart[productIndex].quantity = 1;
        }
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    updateCartDisplay();
}

// Função para remover item do carrinho
function removeFromCart(productName) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    cart = cart.filter(item => item.name !== productName);
    localStorage.setItem('cart', JSON.stringify(cart));
    updateCartDisplay();
}

// Função para limpar o carrinho
function clearCart() {
    localStorage.removeItem('cart');
    updateCartDisplay();
}

// Atualiza o carrinho quando a página carregar
document.addEventListener('DOMContentLoaded', updateCartDisplay);

async function getCategories() {
    const response = await fetch("https://localhost:8000/api/product/getcategories?userId=13");
    const categoriesWithProducts = await response.json();

    const tableBody = document.querySelector('#pricesTable tbody');

    categoriesWithProducts.forEach(category => {
        const row = tableBody.insertRow();

        const categoryNameCell = row.insertCell();
        categoryNameCell.textContent = category.categoryName;

        const retailers = ["Aldi", "Asda", "Morrisons"];
        retailers.forEach(retailer => {
            const retailerProducts = category.products.filter(product => product.retailerName === retailer);

            const retailerCell = row.insertCell();

            if (retailerProducts.length > 0) {
                const productsList = document.createElement('tr');
                retailerProducts.forEach(product => {
                    const productItem = document.createElement('td');
                    productItem.textContent = `£${product.price}`;
                    productsList.appendChild(productItem);
                });

                retailerCell.appendChild(productsList);
            } else {
                retailerCell.textContent = "-";
            }
        });
    });
}
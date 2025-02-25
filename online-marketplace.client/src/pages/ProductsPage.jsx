import { useEffect, useState } from "react";

function ProductsPage() {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchProducts() {
            try {
                const response = await fetch("https://localhost:7155/api/Product"); // ✅ Ensure correct API URL

                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }

                const data = await response.json();
                setProducts(data);
            } catch (err) {
                console.error("Error fetching products:", err);
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }

        fetchProducts();
    }, []);

    const addToCart = (product) => {
        let cart = JSON.parse(localStorage.getItem("cart")) || [];
        const existingItem = cart.find((item) => item.id === product.id);

        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            cart.push({ ...product, quantity: 1 });
        }

        localStorage.setItem("cart", JSON.stringify(cart));
        alert(`${product.name} added to cart!`);
    };

    return (
        <div className="products-page">
            <h1>Products</h1>

            {loading ? <p>Loading products...</p> : error ? <p style={{ color: "red" }}>Error: {error}</p> : (
                <div className="product-list">
                    {products.length === 0 ? <p>No products available.</p> : (
                        products.map((product) => (
                            <div key={product.id} className="product-card">
                                <img src={product.imageUrl || "https://via.placeholder.com/150"}
                                    alt={product.name} />
                                <h3>{product.name}</h3>
                                <p>{product.description}</p>
                                <p>${product.price?.toFixed(2) || "0.00"}</p>
                                <button onClick={() => addToCart(product)}>Add to Cart</button>
                            </div>
                        ))
                    )}
                </div>
            )}
        </div>
    );
}

export default ProductsPage;

import  { useEffect, useState } from "react";
import "./ProductsGrid.css"; 

function ProductsGrid() {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchProducts() {
            try {
                const response = await fetch("https://localhost:7155/api/Product");
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                const data = await response.json();
                setProducts(data);
            } catch (error) {
                console.error("Error fetching products:", error);
                setError(error.message);
            } finally {
                setLoading(false);
            }
        }
        fetchProducts();
    }, []);

    return (
        <div className="products-container">
            <h1 className="title">Available Products</h1>

            {loading ? (
                <p className="loading">Loading products...</p>
            ) : error ? (
                <p className="error">Error: {error}</p>
            ) : (
                <div className="product-grid">
                    {products.map((product) => (
                        <div key={product.id} className="product-card">
                            <img src={product.imageUrl} alt={product.name} className="product-image" />
                            <h3 className="product-title">{product.name}</h3>
                            <p className="product-description">{product.description}</p>
                            <p className="product-price">${product.price.toFixed(2)}</p>
                            <button className="buy-btn">Buy Now</button>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}

export default ProductsGrid;

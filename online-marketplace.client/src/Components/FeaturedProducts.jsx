// src/components/FeaturedProducts.jsx
import React, { useEffect, useState } from 'react';

function FeaturedProducts() {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        async function fetchFeatured() {
            try {
                const response = await fetch('/api/products/featured');
                if (response.ok) {
                    const data = await response.json();
                    setProducts(data);
                } else {
                    console.error("Failed to load featured products");
                }
            } catch (error) {
                console.error("Error fetching featured products", error);
            }
        }
        fetchFeatured();
    }, []);

    return (
        <div className="featured-products">
            <h2>Featured Products</h2>
            <div className="product-grid">
                {products.length === 0 ? (
                    <p>No featured products available at the moment.</p>
                ) : (
                    products.map(product => (
                        <div key={product.id} className="product-card">
                            <img src={product.imageUrl} alt={product.name} />
                            <h3>{product.name}</h3>
                            <p>{product.description}</p>
                            <p>${product.price}</p>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
}

export default FeaturedProducts;

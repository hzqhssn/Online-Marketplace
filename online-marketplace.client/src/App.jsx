import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

// Import page components
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import ProductsPage from './pages/ProductsPage';
import OrderPage from './pages/OrderPage';
import ProfilePage from './pages/ProfilePage';

function App() {
    return (
        <Router>
            <Routes>
                {/* Home page with header, search bar, featured products, and footer */}
                <Route path="/" element={<HomePage />} />

                {/* Login page for authentication */}
                <Route path="/login" element={<LoginPage />} />

                {/* Products page - you can create a page to list all products */}
                <Route path="/products" element={<ProductsPage />} />

                {/* Orders page - displays order history or order details */}
                <Route path="/orders" element={<OrderPage />} />

                {/* Profile page - shows user details, settings, etc. */}
                <Route path="/profile" element={<ProfilePage />} />
            </Routes>
        </Router>
    );
}

export default App;

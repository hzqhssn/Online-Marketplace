// src/components/Header.jsx
import React from 'react';
import { Link } from 'react-router-dom';

function Header() {
    return (
        <header className="header">
            <div className="logo">
                <Link to="/">Online Marketplace</Link>
            </div>
            <nav className="navigation">
                <ul>
                    <li><Link to="/">Home</Link></li>
                    <li><Link to="/products">Products</Link></li>
                    <li><Link to="/orders">Orders</Link></li>
                    <li><Link to="/profile">Profile</Link></li>
                    <li><Link to="/login">Login</Link></li>
                </ul>
            </nav>
        </header>
    );
}

export default Header;

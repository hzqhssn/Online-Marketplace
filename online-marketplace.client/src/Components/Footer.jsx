// src/components/Footer.jsx
import React from 'react';

function Footer() {
    return (
        <footer className="footer">
            <p>&copy; {new Date().getFullYear()} Online Marketplace. All rights reserved.</p>
        </footer>
    );
}

export default Footer;

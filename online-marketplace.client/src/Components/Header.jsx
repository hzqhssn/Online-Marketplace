import { Link } from 'react-router-dom';
import "./Header.css";

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
                    <li>
                        <Link to="/cart" className="cart-btn">
                            🛒 Go to Cart
                        </Link>
                    </li>
                </ul>
            </nav>
        </header>
    );
}

export default Header;

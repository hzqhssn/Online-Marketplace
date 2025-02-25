import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Layout from './components/Layout'; 
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import ProductsPage from './pages/ProductsPage';
import OrderPage from './pages/OrderPage';
import ProfilePage from './pages/ProfilePage';
import SuccessPage from './pages/SuccessPage';
import CartPage from './pages/CartPage';
import CheckoutPage from './pages/CheckoutPage';
import PaymentPage from './pages/PaymentPage';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<HomePage />} />
                    <Route path="login" element={<LoginPage />} />
                    <Route path="products" element={<ProductsPage />} />
                    <Route path="orders" element={<OrderPage />} />
                    <Route path="profile" element={<ProfilePage />} />
                    <Route path="cart" element={<CartPage />} />
                    <Route path="checkout" element={<CheckoutPage />} />
                    <Route path="payment" element={<PaymentPage />} />
                    <Route path="success" element={<SuccessPage />} />
                </Route>
            </Routes>
        </Router>
    );
}

export default App;

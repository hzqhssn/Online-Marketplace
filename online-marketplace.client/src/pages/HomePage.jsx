// src/pages/HomePage.jsx
import React from 'react';
import Header from '../components/Header';
import SearchBar from '../components/SearchBar';
import FeaturedProducts from '../components/FeaturedProducts';
import Footer from '../components/Footer';

function HomePage() {
    return (
        <div className="homepage">
            <Header />
            <SearchBar />
            <FeaturedProducts />
            <Footer />
        </div>
    );
}

export default HomePage;

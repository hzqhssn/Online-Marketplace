import React from 'react';
import SearchBar from '../components/SearchBar';
import FeaturedProducts from '../components/FeaturedProducts';
import Footer from '../components/Footer';

function HomePage() {
    return (
        <div className="homepage">
            <SearchBar />
            <FeaturedProducts />
            <Footer />
        </div>
    );
}

export default HomePage;

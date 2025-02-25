import React from "react";
import Header from "./Header";
import { Outlet } from "react-router-dom";

function Layout() {
    return (
        <>
            <Header /> {/* ✅ Ensures Header is always present */}
            <main>
                <Outlet /> {/* ✅ This renders the current page content */}
            </main>
        </>
    );
}

export default Layout;

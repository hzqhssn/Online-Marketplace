import  { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function ProfilePage() {
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("userId");

        if (!token || !userId) {
            navigate("/login"); // Redirect to login if not authenticated
            return;
        }

        async function fetchUser() {
            try {
                const response = await fetch(`https://localhost:7155/api/User/${userId}`, {
                    method: "GET",
                    headers: { "Authorization": `Bearer ${token}` },
                });

                if (!response.ok) {
                    throw new Error("Failed to fetch user data");
                }

                const data = await response.json();
                setUser(data);
            } catch (error) {
                console.error("Error fetching profile:", error);
                navigate("/login");
            }
        }

        fetchUser();
    }, [navigate]);

    return (
        <div className="profile-container">
            <h1>Profile</h1>
            {user ? (
                <div className="profile-info">
                    <p><strong>Name:</strong> {user.name}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                    <p><strong>Phone:</strong> {user.phone || "Not provided"}</p>
                </div>
            ) : (
                <p>Loading profile...</p>
            )}
        </div>
    );
}

export default ProfilePage;

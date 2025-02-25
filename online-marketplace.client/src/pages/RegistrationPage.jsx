import  { useState } from "react";

function RegisterPage() {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        phone: "",
        password: "",
    });

    const [message, setMessage] = useState(null);

    const handleInputChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("https://localhost:7155/api/Auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: formData.name,
                    email: formData.email,
                    phone: formData.phone,
                    passwordHash: formData.password, // 🔹 Must match backend naming
                }),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Registration failed");
            }

            setMessage("User registered successfully!");
        } catch (error) {
            setMessage(error.message);
        }
    };

    return (
        <div className="register-container">
            <h1>Register</h1>
            {message && <p className="message">{message}</p>}

            <form onSubmit={handleSubmit}>
                <label>Name: <input type="text" name="name" value={formData.name} onChange={handleInputChange} required /></label>
                <label>Email: <input type="email" name="email" value={formData.email} onChange={handleInputChange} required /></label>
                <label>Phone: <input type="tel" name="phone" value={formData.phone} onChange={handleInputChange} required /></label>
                <label>Password: <input type="password" name="password" value={formData.password} onChange={handleInputChange} required /></label>
                <button type="submit">Register</button>
            </form>
        </div>
    );
}

export default RegisterPage;

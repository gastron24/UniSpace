import { useEffect, useState } from 'react';
import api from '../api/client';

export default function ProfilePage() {
    const [user, setUser] = useState<{ email: string } | null>(null);

    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const response = await api.get('/users/profile');
                setUser(response.data);
            } catch (err) {
                console.error('Не удалось загрузить профиль');
            }
        };
        fetchProfile();
    }, []);

    return (
        <div style={{ padding: '20px' }}>
            <h2>Личный кабинет</h2>
            {user ? <p>Привет, {user.email}!</p> : <p>Загрузка...</p>}
            <button
                onClick={() => {
                    localStorage.removeItem('token');
                    window.location.href = '/login';
                }}
                style={{ marginTop: '20px', padding: '8px 16px' }}
            >
                Выйти
            </button>
        </div>
    );
}
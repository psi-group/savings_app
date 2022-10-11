import { useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';


const RestaurantDetails = () => {
    const params = useParams()

    console.log(params.id);

    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch(`https://localhost:7183/api/restaurants/` + params.id)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                return response.json();
            })
            .then((actualData) => {
                setData(actualData);
                setError(null);
            })
            .catch((err) => {
                setError(err.message);
                setData(null);
            })
            .finally(() => {
                setLoading(false);
            });
    }, [])

    return (
        <ul>
            {data &&
                data.name
                }
        </ul>
     )
}

export default RestaurantDetails
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const API_KEY = '411c4fa2-cb9a-4222-b5bc-a38f1cfa43f5';

interface Breed {
    id: string;
    name: string;
    image: {
        url: string
    }
}

export const catApiSlice = createApi({
    reducerPath: 'api',
    baseQuery: fetchBaseQuery({
        baseUrl: "https://api.thecatapi.com/v1",
        prepareHeaders(headers) {
            headers.set('x-api-key', API_KEY);
            return headers;
        }
    }),
    endpoints(builder) {
        return {
            fetchBreeds: builder.query<Breed[], number | void>({
                query(limit = 10) {
                    return `/breeds?limit=${limit}`;
                },
            }),
        };
    },
});

export const { useFetchBreedsQuery } = catApiSlice;

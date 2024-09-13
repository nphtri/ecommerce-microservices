import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export interface BannerImage {
  id: number;
  description: string;
  image: string;
}

export const bannerApi = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://localhost:6001/api/banners',
  }),
  endpoints(builder) {
    return {
      fetchBanner: builder.query<BannerImage[], void>({
        query: () => `/`,
      }),
    };
  },
});

export const { useFetchBannerQuery } = bannerApi;

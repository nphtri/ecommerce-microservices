import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import { bannerApi } from '../features/banner/banner-api-slice';
import bannerReducer from '../features/banner/banner-slice';

export const store = configureStore({
  reducer: {
    banner: bannerReducer,
    [bannerApi.reducerPath]: bannerApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(bannerApi.middleware);
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;

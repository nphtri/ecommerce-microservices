import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { BannerImage } from './banner-api-slice';

interface BannerState {
  images: BannerImage[];
  index: number;
}

const initialState: BannerState = {
  images: [],
  index: 0,
};

const bannerSlice = createSlice({
  name: 'banner',
  initialState,
  reducers: {
    loadBanner: (state, action: PayloadAction<BannerImage[]>) => {
      state.images = action.payload;
    },
    left: (state) => {
      if (state.index > 0) state.index--;
      else state.index = state.images.length - 1;
    },
    right: (state) => {
      if (state.index < state.images.length - 1) state.index++;
      else state.index = 0;
    },
  },
});

export const { loadBanner, left, right } = bannerSlice.actions;
export default bannerSlice.reducer;

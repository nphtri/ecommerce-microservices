import React, { useEffect } from 'react';
import mockNews from '../assets/images/mock/news-3.jpg';
import mockCard from '../assets/images/mock/news-1.jpg';
import mockCard2 from '../assets/images/mock/news-2.jpg';
import mockCard3 from '../assets/images/mock/news-4.jpg';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { loadBanner, right } from '../features/banner/banner-slice';
import { BannerImage, useFetchBannerQuery } from '../features/banner/banner-api-slice';

export default function Home() {
  const banner = useAppSelector((state) => state.banner);
  const dispatch = useAppDispatch();
  function handleClick() {
    console.log('clicked');
    dispatch(right());
  }
  function getImage(): BannerImage {
    return banner.images != undefined && banner.images.length > 0
      ? banner.images[banner.index]
      : { id: -1, image: '', description: '' };
  }
  const { data } = useFetchBannerQuery();
  useEffect(() => {
    if (data != undefined) {
      dispatch(loadBanner(data));
    }
  }, [data]);
  return (
    <React.Fragment>
      <section id="banner-section">
        <div
          className="banner-holder"
          onClick={handleClick}
          style={{
            backgroundImage: `url(${getImage().image})`,
          }}
        >
          <div className="banner-title">
            <span>{getImage().description}</span>
          </div>
        </div>
        <div className="banner-index-bar">
          {banner.images.map((val, i) => {
            return (
              <span
                key={`banner-${val.id}`}
                className="dot"
                style={{ backgroundColor: i != banner.index ? '#bbb' : '#fff' }}
              ></span>
            );
          })}
        </div>
      </section>
      <section id="news-section" className="container">
        <div className="news-section-body">
          <div className="left">
            <div className="section-title news-section-title">
              <span>News</span>
            </div>
            <div className="current-news">
              <div className="img-holder">
                <img src={mockNews} alt="news-img" />
              </div>
              <div className="news-detail">
                <div className="news-title">
                  <span>Illustration art lead on LoR at Riot Games</span>
                </div>
                <div className="news-content">
                  <p>
                    Lorem Ipsum chỉ đơn giản là một đoạn văn bản giả, được dùng vào việc trình bày và dàn trang phục vụ
                    cho in ấn. Lorem Ipsum đã được sử dụng như một văn bản chuẩn cho ngành công nghiệp in ấn từ những
                    năm 1500, khi một họa sĩ vô danh ghép nhiều đoạn văn bản với nhau để tạo thành một bản mẫu văn bản.
                    Đoạn văn bản này không những đã tồn tại năm thế kỉ, mà khi được áp dụng vào tin học văn phòng, nội
                    dung của nó vẫn không hề bị thay đổi. Nó đã được phổ biến trong những năm 1960 nhờ việc bán những
                    bản giấy Letraset in những đoạn Lorem Ipsum, và gần đây hơn, được sử dụng trong các ứng dụng dàn
                    trang, như Aldus PageMaker.
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className="right">
            <div className="news-card">
              <img src={mockCard3} alt="card" className="img-contain" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
            <div className="news-card">
              <img src={mockCard} alt="card" className="img-contain" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
            <div className="news-card">
              <img src={mockCard2} className="img-contain" alt="card" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
            <div className="news-card">
              <img src={mockCard3} alt="card" className="img-contain" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
            <div className="news-card">
              <img src={mockCard} alt="card" className="img-contain" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
            <div className="news-card">
              <img src={mockCard2} alt="card" className="img-contain" />
              <span className="news-card-title">Ashno Alice - Leisure</span>
            </div>
          </div>
        </div>
      </section>
      <section id="product-hightlighs-section" className="container">
        <div className="section-title">
          <span>Product Highlights</span>
        </div>
        <div className="highlights-body">
          <div className="highlight-card">
            <div className="highlight-image-holder">
              <img src={mockCard2} alt="highligh-card-img" className="img-covered" />
            </div>
            <div className="highlight-card-details">
              <div className="highlight-card-title">
                <span>CURATE & ELEVATE</span>
              </div>
              <div className="highlight-card-description">
                <span>
                  We think it's more important than ever to curate and elevate, bringing you limited editions from some
                  of the most fabulous artists and creatives on earth... Read More
                </span>
              </div>
            </div>
          </div>

          <div className="highlight-card">
            <div className="highlight-image-holder">
              <img src={mockCard3} alt="highligh-card-img" className="img-covered" />
            </div>
            <div className="highlight-card-details">
              <div className="highlight-card-title">
                <span>CURATE & ELEVATE</span>
              </div>
              <div className="highlight-card-description">
                <span>
                  We think it's more important than ever to curate and elevate, bringing you limited editions from some
                  of the most fabulous artists and creatives on earth... Read More
                </span>
              </div>
            </div>
          </div>

          <div className="highlight-card">
            <div className="highlight-image-holder">
              <img src={mockCard} alt="highligh-card-img" className="img-covered" />
            </div>
            <div className="highlight-card-details">
              <div className="highlight-card-title">
                <span>CURATE & ELEVATE</span>
              </div>
              <div className="highlight-card-description">
                <span>
                  We think it's more important than ever to curate and elevate, bringing you limited editions from some
                  of the most fabulous artists and creatives on earth... Read More
                </span>
              </div>
            </div>
          </div>
          <div className="highlight-card">
            <div className="highlight-image-holder">
              <img src={mockNews} alt="highligh-card-img" className="img-covered" />
            </div>
            <div className="highlight-card-details">
              <div className="highlight-card-title">
                <span>CURATE & ELEVATE</span>
              </div>
              <div className="highlight-card-description">
                <span>
                  We think it's more important than ever to curate and elevate, bringing you limited editions from some
                  of the most fabulous artists and creatives on earth... Read More
                </span>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section id="arts-section" className="container">
        <div className="section-title">
          <span>Arts</span>
          <nav className="arts-type-nav">
            <ul>
              <li>
                <a href="#">Concepture</a>
              </li>
              <li>
                <a href="#">Character</a>
              </li>
              <li>
                <a href="#">Creatures & Monsters</a>
              </li>
              <li>
                <a href="#">Nature</a>
              </li>
            </ul>
          </nav>
        </div>
        <div className="arts-section-container">
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard3} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard3} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard3} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard2} alt="art" className="img-covered" />
            </div>
          </div>
          <div className="art-card">
            <div className="art-card-img-holder">
              <img src={mockCard} alt="art" className="img-covered" />
            </div>
          </div>
        </div>
      </section>
      <section id="artists-section" className="container">
        <div className="section-title">
          <span>Artists</span>
        </div>
        <div className="artists-section-container">
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard3} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard2} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
          <div className="artist-card">
            <div className="artist-card-img-holder">
              <img src={mockCard} alt="artist" className="img-covered" />
            </div>
          </div>
        </div>
      </section>{' '}
    </React.Fragment>
  );
}

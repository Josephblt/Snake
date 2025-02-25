using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake.GameObjects
{
    public class FoodManager
    {
        #region Constructor

        public FoodManager(Field field)
        {
            _foodsToAdd = new List<FoodEntity>();
            _foods = new List<FoodEntity>();
            _foodsToRemove = new List<FoodEntity>();
            _spawnTimer = _spawnTimeLimit;
            _field = field;
        }

        #endregion

        #region Private Fields

        private Field _field;

        private List<FoodEntity> _foods;
        private List<FoodEntity> _foodsToAdd;
        private List<FoodEntity> _foodsToRemove;

        private float _spawnTimer;
        private float _spawnTimeLimit = 500f;

        #endregion

        #region Public Methods

        public void CheckForCollision(SnakeEntity snake)
        {
            foreach(FoodEntity food in _foods)
                if (snake.Collides(food))
                {
                    snake.SolveCollision(snake);
                    food.SolveCollision(snake);
                    SpawnFood();
                }
        }

        public void DestroyFood(FoodEntity entity)
        {
            _foodsToRemove.Add(entity);
        }

        public void Render(System.Drawing.Graphics graphics)
        {
            foreach (FoodEntity food in _foods)
                food.Render(graphics);
        }

        public void SpawnFood()
        {
            _spawnTimer = 0;
            Random random = new Random();

            float radius = 10f;
            float x = random.Next((int)(_field.Location.X + (radius * 2)), (int)(_field.Location.X + _field.Size.Width - (radius * 2)));
            float y = random.Next((int)(_field.Location.Y + (radius * 2)), (int)(_field.Location.Y + _field.Size.Height - (radius * 2)));

            FoodEntity food = new FoodEntity(new PointF(x, y), 10f, 500f);
            _foodsToAdd.Add(food);
        }

        public void Update(float deltaTime)
        {
            _spawnTimer += deltaTime;
            if (_spawnTimer >= _spawnTimeLimit)
                SpawnFood();

            foreach (FoodEntity food in _foodsToRemove)
                _foods.Remove(food);
            _foodsToRemove.Clear();

            foreach (FoodEntity food in _foodsToAdd)
                _foods.Add(food);
            _foodsToAdd.Clear();

            foreach (FoodEntity food in _foods)
                food.Update(deltaTime);
        }

        #endregion
    }
}

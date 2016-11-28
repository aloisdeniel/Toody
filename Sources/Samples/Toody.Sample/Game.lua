require 'SampleModule';

local sprites = {};
local sprite;
local tween, tween2, tween3;

function load(content)
	texture = content.getTexture("assets.png");
	sprite = texture.CreateSprite();
	table.insert(sprites, sprite);

 	tween = Tween.float(0.0 , 6.0 , 1.0 , Easing.easeBoth, Repeat.loopWithReverse);
 	tween2 = Tween.point({0,0}, {200,200}, 1, Easing.easeBoth, Repeat.loopWithReverse);
 	tween3 = Tween.color("#FFFFFFFF","55FF0000", 2, Easing.easeBoth, Repeat.loopWithReverse);
end

function update(camera, delta)
    tween.update(delta);
    tween2.update(delta);
    tween3.update(delta);
    sprite.rotation = tween.value;
    sprite.destination = tween2.value;
    sprite.color = tween3.value;
end

function draw(renderer)
	renderer.Draw(sprites);
end